import { createContext } from "react";
import { EMPTY_GUID } from "@common/Constants";

const store = localStorage.getItem('store') as string;

export const initialUserState = {
  isLoggedIn: false,
  userId: EMPTY_GUID,
  profile: {
    profileId: EMPTY_GUID,
    firstName: '',
    lastName: '',
    email: '',
    photoThumbUrl: '',
    roles: '{}'
  }
}

export const initialUserLocation = {
  location: {
    countryCode: '',
    ip: ''
  }
}

export const initialState = JSON.parse(store || JSON.stringify({
  location: {
    countryCode: '',
    ip: ''
  }
}));


export default createContext<any>(initialState);