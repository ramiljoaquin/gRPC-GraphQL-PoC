import React, { useLayoutEffect, useEffect, useReducer, useState } from 'react';
import LayoutContext, { initialState, initialUserState, initialUserLocation } from './LayoutContext';

interface ContentProps {
  children: any;
}

const Provider = ({ children }: ContentProps) => {
  const [state, dispatch] = useReducer((state: any, action: any) => {
    switch (action.type) {
      case 'LOCATION_STATE': {
        return {
          ...state,
          location: {
            ...state.location,
            ...action.payload
          }
        };
      }
      default: {
        return state;
      }
    }
  }, initialState);

  useLayoutEffect(() => {

  }, []);


  useEffect(() => {
    localStorage.setItem('store', JSON.stringify(state));
  }, [state]);

  return (
    <LayoutContext.Provider value={{ ...state, dispatch }}>
      {children}
    </LayoutContext.Provider>
  );
};

export default Provider;

export const Context = LayoutContext;

export const UserState = initialUserState;

export const useUserState = () => {
  const [userState, setUserState] = useState<any>(initialUserState);
  useEffect(() => {
    localStorage.setItem(
      'store',
      JSON.stringify({
        ...initialState,
        user: {
          ...userState,
        },
      }),
    );
  }, [userState]);
  return [userState, setUserState];
};

export const useUserLocation = () => {
  const [userLocation, setUserLocation] = useState<any>(initialUserLocation);
  useEffect(() => {
    localStorage.setItem(
      'store',
      JSON.stringify({
        ...initialState,
        location: {
          ...userLocation,
        },
      }),
    );
  }, [userLocation]);
  return [userLocation, setUserLocation];
};
