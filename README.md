# Order Pricing System

## Overview
E-commerce pricing API with quantity-based discounts and tax calculation.

## Project Structure
- `Services/PricingService.cs` - Pricing logic with discount and tax calculations
- `Models/OrderRequest.cs` - Input model for API requests
- `Models/Product.cs` - Product model
- `Models/PricingResponse.cs` - Output model for API responses
- `products.json` - Product catalog
- `Controllers/PricingController.cs` - API endpoint

## Requirements
- .NET 8.0 SDK
- No database required; uses `products.json` file

## How to Run
1. Ensure .NET 8.0 SDK is installed
2. Clone repository:
git clone https://github.com/proshevav/OrderPricing.git
3. Run project:
dotnet run
4. API runs at: `http://localhost:5174`


## API Endpoint
GET /api/pricing/calculate?productId=PROD-001&quantity=55&country=MK
Returns JSON with subtotal, discount, tax, and final price.

## Test Cases

### Test Case 1
Input: `productId=PROD-001`, `quantity=55`, `country=MK`  
Output: `finalPrice = 700.92`

### Test Case 2
Input: `productId=PROD-001`, `quantity=100`, `country=DE`  
Output: `finalPrice = 1224.00`

### Test Case 3
Input: `productId=PROD-001`, `quantity=25`, `country=USA`  
Output: `finalPrice = 330.00`

Test case 1: [Run in browser](http://localhost:5174/api/pricing/calculate?productId=PROD-001&quantity=55&country=MK)


Test case 2: [Run in browser](http://localhost:5174/api/pricing/calculate?productId=PROD-001&quantity=100&country=DE)

Test case 3: [Run in browser](http://localhost:5174/api/pricing/calculate?productId=PROD-001&quantity=25&country=USA)

## Bugs Fixed
- Fixed discount calculation logic (discount only applies if subtotal ≥ 500)
- Corrected tax calculation order (to be after discount)
- Implemented product JSON loading
- Fixed null warnings for non-nullable properties (required)


