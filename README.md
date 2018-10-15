# Com.Service.TaxCalculation

*REQUIREMENT: DOCKER


1.USAGE for docker:

	*run docker apps.
	
	*download this repository and then open in cmd :
		*Input docker command : docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
		*after downloading image complete,build image input docker command: docker-compose -f docker-compose.yml up -d

	*alternate option : 
		*open Com.Service.TaxCalculation.Lib.sln using visual studio code 2017 or above
		*right click on docker-compose "set as startup project" and then press start button


2.API DOCUMENTATION:

	*endpoint:http://localhost:5055/v1/
	
	*Product WebApi service:
		*uri:product
		*service: endpoint/uri : http://localhost:5055/v1/product
		*usage:   http://localhost:5055/v1/product
			*POST example (properties: Name (string), TaxCode(int) accept JSON:
			
				{
			        	"Name" :"BigMac",
       					"TaxCode": 1,       				
				}

			*GET (get all data): 	http://localhost:5055/v1/product

			*GET (get data by id) : http://localhost:5055/v1/product/id
				*example: http://localhost:5055/v1/product/1

	*TaxCalculation WebApi service:
		*uri:tax-calculation
		*service: endpoint/uri : http://localhost:5055/v1/tax-calculation
		*usage:   http://localhost:5055/v1/tax-calculation
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

