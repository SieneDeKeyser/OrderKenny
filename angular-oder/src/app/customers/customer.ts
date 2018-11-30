export class Customer {
    id?: string;
    firstName: string;
    lastName: string;
    email: Email;
    address: Address;
    phoneNumber: PhoneNumber;
    constructor(){
        this.address = new Address();
        this.email = new Email();
        this.phoneNumber = new PhoneNumber();
    }

  }

  class Email {
     localPart: string;
     domain: string;
     complete: string;
  }

  class Address {
       streetName: string;
       houseNumber: string;
       postalCode: string;
       country: string;
   }

   class PhoneNumber {
    number: string;
    countryCallingCode: string;
    }
