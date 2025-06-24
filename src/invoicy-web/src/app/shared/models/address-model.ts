export interface AddressResponseModel {
  id: number;
  province: string;
  city: string;
  suburb?: string | null;
  street: string;
  zipCode: string;
}

export interface AddressRequestModel {
  province: string;
  city: string;
  suburb?: string | null;
  street: string;
  zipCode: string;
}
