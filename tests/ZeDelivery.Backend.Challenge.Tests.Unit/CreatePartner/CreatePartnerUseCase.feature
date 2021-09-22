Feature: CreatePartnerUseCase
	Use case execution

@mytag
Scenario: Inserir um novo Partner com sucesso
	Given a valid partner as input 
	And the use case is executed
	And no errors occurs at input data validation
	And the partner has not already registered
	When the partner is inserted in the database
	And the result should be partner created