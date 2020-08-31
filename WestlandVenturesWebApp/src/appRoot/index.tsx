import React, { useState, Suspense } from 'react';
import { BrowserRouter, Route, Switch, Redirect } from 'react-router-dom';
import { RouteProps, RedirectProps } from 'react-router';
import { useQuery } from '@apollo/client';
import { useCookies } from 'react-cookie';
import { isEmpty } from 'lodash';
import publicRedirects from './redirects/public';
import privateRedirects from './redirects/private';
import publicRoutes from './routes/public';
import privateRoutes from './routes/private';
import {  EMPTY_GUID } from '@common/Constants';
import { GET_PROFILE, GET_IP_ADDRESS } from '@common/helpers/Account/gqlTags';
import LayoutProvider, { Context, useUserLocation } from '@src/hoc/Layout';
import MainLayout from '@components/Layouts/Main';
import EmptyLayout from '@components/Layouts/Empty';
import Loader from '@components/Loader';


export default () => {
  const [cookies] = useCookies(['user_id']);
  const [, setUserLocation] = useUserLocation();
  const [userState, setUserState] = useState<any>({
    isLoggedIn: false,
    userId: EMPTY_GUID,
    profile: {
      profileId: EMPTY_GUID
    }
  })
  const userId = cookies['user_id'] as string;

  const { loading: loadingIp } = useQuery(GET_IP_ADDRESS, {
    onCompleted: ({ getIpAddress }: any) => {
      const { country_code='', IPv4='' } = getIpAddress?.data;
      setUserLocation({
        countryCode: country_code,
        ipAddress: IPv4
      })
    }
  })

  const { loading } = useQuery(GET_PROFILE, {
    skip: isEmpty(userId),
    onCompleted: ({ profile }: any) => {
      setUserState({
        userId: profile.profileId,
          isLoggedIn: true,
          profile: {
            ...profile,
          }
      });
    },
    variables: {
      profileId: userId,
    },
    fetchPolicy: 'cache-and-network'
  });


  if (loading || loadingIp) return <Loader  loading={true} />;

  const AdminLayout = userState.isLoggedIn ? MainLayout : EmptyLayout;
  const mainRoutes: RouteProps[] = userState.isLoggedIn ? privateRoutes : publicRoutes;
  const mainRedirects: RedirectProps[] = userState.isLoggedIn
    ? privateRedirects
    : publicRedirects;

  return (
    <BrowserRouter>
    <Suspense fallback={<div>Loading...</div>}>
      <LayoutProvider>
        <Context.Consumer>
          {(context: any) => (
            <AdminLayout {...context} user={userState}>
              <Switch>
                {mainRedirects.map(
                  (redirect: RedirectProps, index: number) => (
                    <Redirect
                      from={redirect.from}
                      to={redirect.to}
                      exact={redirect.exact}
                      key={String(index)}
                    />
                  ),
                )}
                {mainRoutes.map((route: any, index: number) => (
                  <Route
                    key={index.toString()}
                    path={route.path}
                    exact={route.exact}
                    render={(props: any) => (
                      <route.component {...context} user={userState} {...props} />
                    )}
                  />
                ))}
              </Switch>
            </AdminLayout>
          )}
        </Context.Consumer>
      </LayoutProvider>
    </Suspense>
  </BrowserRouter>
  );
};
