# clash detection server

this started from a bim assignment, but i approached it as if it was a backend component that other systems would call and depend on.

the point was not only to make the rule checks work, but to shape the solution in a clean way so that:

- new rules can be added without rewriting the flow
- different search / indexing implementations can be introduced later
- the api contract stays clear for integration with other applications

the clash detection application serves as a backend component and should provide a clear and usable api for other systems.

## why i started this way

for this kind of problem, the rules are only one part of it.

the other part is the structure around the rules.

if the code starts growing without separation between api, domain, orchestration, and rule execution, then every new requirement becomes harder to add and harder to maintain.

so the focus here was:

1. keep the controller thin
2. move orchestration into a manager
3. isolate rules behind interfaces
4. keep geometry logic shared and reusable
5. make the spatial search implementation swappable later

this keeps the solution cleaner and makes it easier to extend.

## current request flow

1. validate json payload regarding fields present and in correct datatype

   - all attributes of a building (`name`, `type`, `width`, `length`, `x`, `y`) must be provided
   - building dimensions (`width`, `length`) must be positive numbers greater than zero
   - positions (`x`, `y`) must be numbers greater than or equal to zero

2. apply business rules on building with siteplan

   -> general business rules, applies to all buildings

   - buildings must be positioned fully within the boundaries of the site plan
   - buildings cannot overlap with each other
   - each building must maintain a minimum clearance distance of `10` units from other buildings

   -> zoning business rules, applies to subset of buildings

   - nightclubs must be at least `200` units away from any school
   - residential buildings must be at least `150` units away from stadiums and nightclubs

3. return a list of detected clashes for other systems to use

   - the current api returns the detected violations after evaluating all rules
   - the intent is to keep the contract clear so other systems can consume it reliably

## architecture

the current repo is structured around small responsibilities.

### `Controllers/`

`ClashDetectionController`

- receives the request
- maps the incoming dto into domain models
- calls the clash detection manager
- returns the detected clashes

### `Contracts/Requests`

request contracts used at the api boundary

- `BuildingRequest`
- `DetectClashesRequest`
- `SitePlanRequest`

this layer exists so validation stays close to the request payload.

### `Contracts/Response`

response contracts returned by the api

- `ViolationDto`
- `DetectClashesResponse`

the important idea here is that the api should expose a clear output contract for downstream consumers.

### `Models/`

domain models

- `Building`
- `SitePlan`
- `BuildingType`

these represent the actual business concepts used by the rules.

### `Managers/`

`ClashDetectionManager`

this is the orchestration layer.

it takes the site plan and the list of buildings, then:

- builds the spatial index service
- executes global rules
- executes zoning rules
- aggregates all detected violations

this keeps business orchestration out of the controller.

### `Interfaces/`

the current extension points are defined here:

- `IClashDetectionManager`
- `IGlobalViolationRule`
- `IZoningViolationRule`
- `ISpatialIndexService`

this is one of the main design choices in the solution.

it allows:

- adding a new rule without changing the whole pipeline
- changing how nearby buildings are searched without rewriting the rules
- keeping the code open for extension in a maintainable way

### `Rules/Global`

rules that apply to all buildings:

- `InsideSiteBoundaryRule`
- `NoOverlapRule`
- `MinimumClearanceRule`

### `Rules/Zoning`

rules that apply only to some building combinations:

- `NightclubSchoolDistanceRule`
- `ResidentialDistanceRule`

this split between global rules and zoning rules is intentional.

it keeps the logic easier to reason about and avoids mixing generic building constraints with specific zoning constraints.

### `Services/`

current spatial search implementation:

- `BruteForceSpatialIndexService`

right now the registered implementation is brute force.

that is acceptable for the current state of the assignment because it keeps the code simple and easy to understand.

### `Shared/`

`BuildingGeometry`

contains shared geometry helpers like:

- overlap checks
- distance calculation

this prevents geometry logic from being duplicated across rules.

## current design decision on spatial search

for now the repo uses brute force spatial search.

that means:

- simple implementation
- easy to read
- easy to validate
- not the most scalable for very large datasets

there should be per zone rule a specific way to index the incoming data, but for the assignment the current choice was to keep it simple first and keep the architecture open for better implementations later.

what can already help a bit, even before advanced indexing, is reducing the candidate list to only the building types at hand for a given zoning rule.

## future scalability idea

this part is not fully implemented in the repo right now, but the architecture was kept open for it.

if this service would be deployed in a more real setup, it could sit behind a proxy / load balancer and run as dockerized instances.

then depending on payload size or configuration, requests could be routed to different server instances using different index strategies.

example idea:

- buildings less than `5000` => docker server using one default indexing strategy
- buildings bigger than `5000` => docker server using another indexing strategy
- buildings bigger than `7000` => docker server using a newer indexing strategy

this is a tradeoff.

we add more complexity at system design level, but we gain:

- flexibility when introducing new algorithms
- better control for testing performance behavior
- cleaner rollout path for new search implementations

note that next to routing based on payload size, traffic percentage routing could also be used to test new algorithms in a controlled way.

## openapi / api contract

the repo currently enables openapi support in development.

that matters because this server is meant to be called by other applications, so the contract should be visible and understandable.

the goal is not only to expose an endpoint, but to expose a usable backend contract.

current endpoint:

- `POST /api/v1/clashes/detect`

input:

- site plan dimensions
- list of buildings with type, dimensions, and position

output:

- detected violations / clashes after rule evaluation

## why i focused on clean code and architecture

the main focus was to make adding new things cheap.

for example:

- adding a new global rule should be mostly creating a new class implementing `IGlobalViolationRule`
- adding a new zoning rule should be mostly creating a new class implementing `IZoningViolationRule`
- changing nearby search behavior should be mostly changing the `ISpatialIndexService` implementation
- the controller should not need to know the details of rule execution

that is the reason the solution is organized this way.

it is not only about passing the current assignment, it is about keeping the code maintainable once the assignment grows.

## current rules implemented

### global rules

- building inside site boundary
- no overlap between buildings
- minimum `10` units clearance between buildings

### zoning rules

- nightclub must be at least `200` units away from school
- residential building must be at least `150` units away from stadiums and nightclubs

## things i would improve next

1. move hardcoded numbers such as `10`, `150`, and `200` into configuration / environment
2. tighten and standardize the response contract
3. add automated tests around request validation and per-rule behavior
4. introduce and benchmark a better spatial index for larger datasets
5. expand the openapi contract with clearer examples for consumers

## testing note

i did not add tests in the current state of the repo.

that said, with the current clean architecture and separation of concerns, adding tests would be straightforward and would cover a good part of the use case and code base.

with an agentic coding workflow, this would be even easier to scale because the code is already split into parts that can be tested in isolation.

for example:

- request contract validation can be tested at the api boundary
- each global rule can be tested independently with focused input scenarios
- each zoning rule can be tested independently with focused building combinations
- the clash detection manager can be tested as an orchestration unit
- the spatial index service can be tested separately from the rule logic

this is one of the reasons i focused on clean structure first, because it makes future testing cheaper and more reliable.

## run locally

```bash
dotnet run
```

the application exposes the clash detection endpoint through the controller and enables openapi in development.

## final note

the main idea behind this repo was:

- solve the assignment
- keep the controller small
- separate orchestration from rule logic
- make rules easy to add
- keep spatial search replaceable later
- expose a backend api that other systems can integrate with

so even though the current implementation stays intentionally simple in some areas, the structure was chosen to keep the solution clean and maintainable as it grows.
