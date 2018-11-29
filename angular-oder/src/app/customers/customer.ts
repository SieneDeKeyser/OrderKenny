export class Customer {
    id?: string;
    firstName: string;
    lastName: string;
    email: Email;
    address: Address;
    phoneNumber: PhoneNumber;

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
