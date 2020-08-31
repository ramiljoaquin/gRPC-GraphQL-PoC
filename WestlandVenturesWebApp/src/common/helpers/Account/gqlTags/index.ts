import { gql } from '@apollo/client';

export const SIGN_UP = gql`
mutation SignUp($signUpRequest: SignUpRequest!) {
  signUp(data: $signUpRequest) {
    userId
    link
  }
}
`;

export const SIGN_IN = gql`
  mutation SignIn($userName: String!, $password: String!) {
    signIn(userName: $userName, password: $password) {
      accessToken
      refreshToken
      expiresIn
      tokenType
    }
  }
`;


export const GET_PROFILE = gql`
  query GetProfile($profileId: ID!) {
    getProfile(profileId: $profileId) {
      profileId
      firstName
      lastName
      email
      website
      photoThumbUrl
      createdWhen
    }
  }
`;

export const GET_IP_ADDRESS = gql`
  query {
    getIpAddress @client
  }
`;

