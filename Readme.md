
### Swagger page: https://3qihjkgruh.execute-api.us-east-2.amazonaws.com/Prod/swagger/index.html
### Controller endpoint: https://3qihjkgruh.execute-api.us-east-2.amazonaws.com/Prod/Loan

The Client App is under the LoanStreet.LoanService.Client.csproj.  The API is deployed to AWS, but you can still run the app locally in visual studio and run the debugger which will start the app locally.  

## Requirements
* Implement the API in the framework or language that you're most comfortable with
* The deliverable shall include a web server that supports the following CRU(D) operations:
    * Create a loan with the following properties as input:
        * Amount
        * Interest rate
        * Length of loan in months
        * Monthly payment amount
    * Get the created loan, by an identifier
    * Update the created loan, by an identifier
* **Out of scope**: Delete, Authentication, Authorization
* The deliverable shall include a sample programmatic client for the aforementioned server  
_See [here](https://github.com/PyGithub/PyGithub) for an example_

_You do not need to provide a web frontend!_

## Extra Credit
* Deploy to the cloud provider of your choice!
* Write your client in a language other than the one used to write the server

## Delivery
_**Please send your submission to eng.interview@loan-street.com**_
## Discussion

This project will assist fellow engineers in getting to know you and your work.
It is intentionally left open-ended to allow you the flexibility to create your
best product using whatever tools you wish.
That said, please do not hesitate to ask questions and request clarifications.

An engineer will debrief and review this code with you as part of the
interview process.  Please be prepared to answer questions such as:
* What are the strengths and weaknesses of ABC framework?
* How is XYZ persistence layer well-suited to your data model?
* What situations may result in failure?
* What considerations have you made in terms of a creating customer facing production application?