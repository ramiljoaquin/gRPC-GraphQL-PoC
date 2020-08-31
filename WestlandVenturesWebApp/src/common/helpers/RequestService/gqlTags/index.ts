import { gql } from '@apollo/client';

export const CREATE_SERVICE_REQUEST = gql`
mutation CreateRequestService($createRequestServiceRequest: CreateRequestServiceRequest!) {
  createRequestService(request: $createRequestServiceRequest) {
    requestServiceId
    title
    description
    serviceTypeId
    serviceType
    createdWhen
    servicestatuses {
      serviceStatusId
      description
      createdWhen
    }
  }
}
`;

export const UPDATE_SERVICE_REQUEST = gql`
mutation UpdateRequestService($updateRequestServiceRequest: UpdateRequestServiceRequest!) {
  updateRequestService(request: $updateRequestServiceRequest) {
    requestServiceId
    title
    description
    createdWhen
    lastEditedWhen 
    servicestatuses {
      serviceStatusId
      description
      createdWhen
      lastEditedWhen
    }
  }
}
`;