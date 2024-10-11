# flash-card
Flash Card API Technology: 
  Back-end: .net 8 , JWT token, SMSglobal, Entity Framework core
  Database: MS Sql Server

	I choose these technology because nowadays, people don't like login with user name and password any more.

	So I choose a way so that user can login with mobile phone number and date of birth.


Set up instructions:

	Under project FlashCard.API, create folder App_Data/key if not exist

	Add PublicSigning.key to App_Data/key

	Rebuild solution

Completed feature : 

	-API CRUD for cards

	-API Request OTP number

	-API Registration using JWT token

	-Middleware to hanlde Exception globally

	-Apply clean architechture, repository and unit of work design pattern, QueryBuilder pattern

	-API caching following configuration
	-Add serilog
Known issue and limitations :
	-Custom Authorize Attribute not working
	-Validate Signature of JWT token got issue due to using Openssl to generate security key not correct
	-so It affect to API CRUD of card when we call API without authentication
	
Future improvement
  -Implement captcha validation
  -Implement mobile number and date of birth validation
  -need to check if the OTPconfig is enabled
  -Create an UI app to integrate
  -implement validation duplicated time when create card
   - Move key to azure key vault
API is publish on azure https://flashcardapi20241010053628.azurewebsites.net/index.html