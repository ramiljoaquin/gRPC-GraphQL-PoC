import { gql } from "@apollo/client";

export default gql`
enum UserType {
  Employer
  HiringLead
  Applicant
}

enum RoleType {
  Administrator
  Manager
  Supervisor
  Editor
  User
}

input SignUpRequest {
  companyName: String
  firstName: String
  lastName: String
  email: String!
  password: String!
  userType: UserType!
  roleType: RoleType
}
`;