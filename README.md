# Viventium

## Specs

You're given test data (DATA.csv) from multiple companies. Design a data store and expose a RESTful API as described below.

The following technologies should be used in the process: 
* ASP.NET WebAPI
* EntityFramework

The final result should be published to GiHub and a link should be shared. 

NOTE: You can use any additional libraries as needed.

## Public API Endpoints

Define the following API Endpoints to upload and access the data: 
### POST /DataStore.

Accepts the CSV data and replaces (clears and imports) the data in the store with the provided one.

Validations:
 
* The employeeNumber should be unique within a given company. 

* The manager of the given employee should exist in the same company.

### GET /Companies

Returns the list of CompanyHeader objects

### GET /Companies/\{companyId\}
 
Returns the Company object by provided ID

### GET /Companies/\{companyId\}/Employees/\{employeeNumber\}
 
Returns the Employee object by provided company ID and employee number