import { AddressResponseModel } from './address-model';

export interface CustomerResponseModel {
  id: number;
  name: string;
  telephoneNumber: string;
  address: AddressResponseModel;
}

import { AddressRequestModel } from './address-model';

export interface CustomerRequestModel {
  name: string;
  telephoneNumber: string;
  address: AddressRequestModel;
}

export interface UpdateCustomerRequestModel {
  id: number;
  name: string;
  telephoneNumber: string;
  address: AddressRequestModel;
}
