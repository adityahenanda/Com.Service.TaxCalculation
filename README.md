# Com.Service.TaxCalculation

*REQUIREMENT: DOCKER


1.USAGE:

	*run docker apps.
	
	*download this repository and then open in cmd :
		*Input docker command : docker-compose -f docker-compose.test.yml docker-compose.override.yml up -d
		*after downloading image complete input docker command: docker build

	*alternate option : 
		*open Com.Service.TaxCalculation.Lib.sln using visual studio code 2017 or above
		*right click on docker-compose "set as startup project" and then press start button
				



2.PROJECT:

	*Com.Service.TaxCalculation.Lib
		*modules for bussiness process : product facade and taxCalculation facade

	*Com.Service.TaxCalculation.Test
		*uni test for controller using moq : product facade and 

	*Com.Service.TaxCalculation.WebApi
		*WebApi controller for each facade : product controller and taxCalculation controller


3.API DOCUMENTATION:

	*endpoint:http://localhost:5055/v1/
	
	*Product WebApi service:
		*uri:product
		*service: endpoint/uri : http://localhost:5055/v1/product
		*usage:  
			*POST example (properties: Name (string), TaxCode(int) accept JSON:
			
				{
			        	"Name" :"BigMac",
       					"TaxCode": 1,       				
				}

			*GET (get all data): 	http://localhost:5055/v1/product

			*GET (get data by id) : http://localhost:5055/v1/product/id
				*example: http://localhost:5055/v1/product/1

	*Product WebApi service:
		*uri:tax-calculation
		*service: endpoint/uri : http://localhost:5055/v1/tax-calculation
		*usage:  
			*POST example :
				(properties: TotalAmount(default 0), 
				             TotalTaxAmount(default 0),
						GrandTotal(default 0, Details 
				(array of product)) accept JSON:

			
				{
      					TotalAmount: 0,
      
					TotalTaxAmount: 0,
      
					GrandTotal: 0,
					Details : [
							{
								"Name":"BigMac",
								"TaxCode":"1",
								"Type":"Food",
								"Amount":1000
							}
						 	]    				
				}


			*GET (get all data): 	http://localhost:5055/v1/tax-calculation

			*GET (get data by id) : http://localhost:5055/v1/tax-calculation/id
				*example: http://localhost:5055/v1/tax-calculation/1

4.DB
	*have 2 table:
	
		1.Product:stores master product
		2.TaxCalculation:stores history transaction (tax calculation)
