# Release Notes - 2026-02-12

## Release Summary
This release introduces the fullstack refactor baseline and workflow ecosystem for `EcommerceWIP`.
It includes backend service-layer refactoring, database hardening with EF migration, frontend stabilization, and CI/CD governance artifacts.

## Included Pull Requests
- #1 `Add multi-agent fullstack workflow ecosystem` (merged into `develop`)
- #2 `Refactor backend and harden database schema` (merged via stacked flow)
- #4 `Refactor frontend and add QA test coverage` (merged via stacked flow)

## Backend Changes
- Added service layer:
  - `ICartService`, `IOrderService`
  - `CartService`, `OrderService`
- Refactored controllers to orchestration-focused flow:
  - `CartController`, `UserOrderController`, `HomeController`
- Hardened repository logic and removed silent exception handling.
- Added application exceptions for consistent error responses:
  - `AppException`, `ValidationException`, `NotFoundException`

## Database Changes
- Migration added:
  - `20260212145611_20260212_refactor_schema_hardening`
- Key changes:
  - `decimal(18,2)` pricing precision for `Book.Price`, `CartDetail.UnitPrice`, `OrderDetail.UnitPrice`
  - Indexes on `ShoppingCart.UserId` (unique) and `Order.UserId`
  - Check constraints for positive quantity and unit price on cart/order details

## Frontend Changes
- Moved inline JS to static files:
  - `wwwroot/js/cart.js`
  - `wwwroot/js/catalog.js`
- Updated Razor views for non-breaking UX cleanup:
  - `Views/Home/Index.cshtml`
  - `Views/Shared/_Layout.cshtml`
  - `Views/Cart/GetUserCart.cshtml`
  - `Views/UserOrder/UserOrders.cshtml`

## Quality and Testing
- Added test project:
  - `Ecommerce.Tests`
- Added xUnit service tests:
  - `CartServiceTests`
  - `OrderServiceTests`
- Local verification passed:
  - `dotnet build Ecommerce.sln --configuration Release`
  - `dotnet test Ecommerce.sln --configuration Release`

## DevOps / Workflow
- Added PR validation, release candidate, staging deploy, production deploy workflows.
- Added CODEOWNERS, issue templates, PR template.
- Added GitFlow/release/migration policies and agent contracts under `docs/`.

## Operational Notes
- Branch protections were restored to strict configuration on `develop` and `main` after merge completion.
- `production` environment approval model remains manual-gated.

## Rollback Guidance
- Application rollback: redeploy prior artifact/revision.
- Schema rollback: use EF command from repository root:
  - `dotnet ef database update <previous_migration> --project Ecommerce --startup-project Ecommerce`
