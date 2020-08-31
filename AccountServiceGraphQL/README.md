# AccountServiceGraphQL
Account Service GraphQL

- [Getting Started](#getting-started)
- [User Guide](#user-guide)
- [DevTools](https://github.com/Ubidy/Ubidy_ContractEmployerGraphQL/blob/master/docs/source/devtools/editor-plugins.md)
- [The schema registry - including CICD](https://github.com/Ubidy/Ubidy_ContractEmployerGraphQL/blob/master/docs/source/graphql-manager/schema-registry.mdx)

## Quick Overview

### System requirements

Before we begin, make sure you have the following installed:

Node.js v8.x or later
npm v6.x or later
VS Code as your editor so you can use Apollo's helpful VS Code extension.

## Sections

- [Getting Started](#getting-started)
- [Philosophy](#philosophy)
- [Support and Contact](#support-and-contact)
- [FAQs](#faqs)
- [Contributing](#contributing)

## Getting Started

Clone the GraphQL API
Now the fun begins! From your /Source/ContractRepos/ development directory, clone this repository:

### Clone with SSH

```sh
git clone git@github.com:Ubidy/Ubidy_ContractEmployerGraphQL.git
```

### Clone with HTTPS

```sh
git clone https://github.com/Ubidy/Ubidy_ContractEmployerGraphQL.git
```

### Installation

Install the npm packages :

```sh
$ npm install
  # or
$ yarn
```

You'll need to have Node v8 or later on your machine. We strongly recommend using NPM v3 or later, or a recent version of Yarn.

Setup your .env variables.

```sh
GRAPHQL_CONTRACTLOOKUPS_API_URL=http://localhost
GRPC_CONTRACTLOOKUPS_API_URL=localhost
GRPC_CONTRACTLOOKUPS_API_PORT=5000
ENGINE_API_KEY=service:contract-employer-graph:GET_THIS_FROM_LASTPASS
SCHEMA_TAG='current'
PORT=4001
```

Now run it using this npm command.

```sh
$ npm run start:dev
  # or
$ yarn start:dev
```

The repository contains three top-level directories inside /src/graphql: resolvers, schema and services.

# Building a schema

We recommend that teams practice Schema First Development

Every graph API is centered around its schema. You can think of a schema as a blueprint that describes all of your data's types and their relationships. A schema also defines what data we can fetch through queries and what data we can update through mutations. It is strongly typed, which unlocks powerful developer tooling.

Schemas are at their best when they are designed around the needs of the clients that are consuming them. Since a schema sits in between your clients and your underlying services, it serves as a perfect middle ground for frontend and backend teams to collaborate.

## Query type

We'll start with the Query type, which is the entry point into our schema that describes what data we can fetch.

The language we use to write our schema is GraphQL's schema definition language (SDL). If you've used TypeScript before, the syntax will look familiar.

# Naming conventions

The GraphQL specification is flexible and doesn't impose specific naming guidelines. However, it's helpful to establish a set of conventions to ensure consistency across your organization. We recommend the following: The GraphQL specification is flexible and doesn't impose specific naming guidelines. However, it's helpful to establish a set of conventions to ensure consistency across your organization. We recommend the following:
`sh`sh

- Field names should use camelCase. Many GraphQL clients are written in JavaScript, Java, Kotlin, or Swift, all of which recommend camelCase for variable names. \* Field names should use camelCase. Many GraphQL clients are written in JavaScript, Java, Kotlin, or Swift, all of which recommend camelCase for variable names.
  @@ -87,3 +87,181 @@ The GraphQL specification is flexible and doesn't impose specific naming guideli
- Enum values should use ALL_CAPS, because they are similar to constants. \* Enum values should use ALL_CAPS, because they are similar to constants.
  ``
  These conventions help ensure that most clients don't need to define extra logic to transform the results returned by your server. These conventions help ensure that most clients don't need to define extra logic to transform the results returned by your server.

Example of a query-driven schema
Let's say we're creating a web app that lists upcoming events in our area. We want the app to show the name, date, and location of each event, along with the weather forecast for it.

In this case, we want our web app to be able to execute a query with a structure similar to the following:

```sh
query EventList {
  upcomingEvents {
    name
    date
    location {
      name
      weather {
        temperature
        description
      }
    }
  }
}
```

Because we know this is the structure of data that would be helpful for our client, that can inform the structure of our schema:

```sh
type Query {
  upcomingEvents: [Event]
}
type Event {
  name: String
  date: String
  location: Location
}
type Location {
  name: String
  weather: WeatherInfo
}
type WeatherInfo {
  temperature: Float
  description: String
}
```

As mentioned, each of these types can be populated with data from a different data source (or multiple data sources). For example, the Event type's name and date might be populated with data from our back-end database, whereas the WeatherInfo type might be populated with data from a third-party weather API.

Designing mutations
In GraphQL, it's recommended for every mutation's response to include the data that the mutation modified. This enables clients to obtain the latest persisted data without needing to send a followup query.

A schema that supports updating the email of a User would include the following:

```sh
type Mutation {
  # This mutation takes id and email parameters and responds with a User
  updateUserEmail(id: ID!, email: String!): User
}
type User {
  id: ID!
  name: String!
  email: String!
}
```

A client could then execute a mutation against the schema with the following structure:

```sh
mutation updateMyUser {
  updateUserEmail(id: 1, email: "jane@example.com"){
    id
    name
    email
  }
}
```

After the GraphQL server executes the mutation and stores the new email address for the user, it responds to the client with the following:

```sh
{
  "data": {
    "updateUserEmail": {
      "id": "1",
      "name": "Jane Doe",
      "email": "jane@example.com"
    }
  }
}
```

Although it isn't mandatory for a mutation's response to include the modified object, doing so greatly improves the efficiency of client code. And as with queries, determining which mutations would be useful for your clients helps inform the structure of your schema.

Structuring mutation responses
A single mutation can modify multiple types, or multiple instances of the same type. For example, a mutation that enables a user to "like" a blog post might increment the likes count for a Post and update the likedPosts list for the User. This makes it less obvious what the structure of the mutation's response should look like.

Additionally, mutations are much more likely than queries to cause errors, because they modify data. A mutation might even result in a partial error, in which it successfully modifies one piece of data and fails to modify another. Regardless of the type of error, it's important that the error is communicated back to the client in a consistent way.

To help resolve both of these concerns, we recommend defining a MutationResponse interface in your schema, along with a collection of object types that implement that interface (one for each of your mutations).

Here's what the MutationResponse interface looks like:

```sh
interface MutationResponse {
  code: String!
  success: Boolean!
  message: String!
}
```

And here's what an implementing object type looks like:

```sh
type UpdateUserEmailMutationResponse implements MutationResponse {
  code: String!
  success: Boolean!
  message: String!
  user: User
}
```

Our updateUserEmail mutation would specify UpdateUserEmailMutationResponse as its return type (instead of User), and the structure of its response would be the following:

```sh
{
  "data": {
    "updateUser": {
      "code": "200",
      "success": true,
      "message": "User email was successfully updated",
      "user": {
        "id": "1",
        "name": "Jane Doe",
        "email": "jane@example.com"
      }
    }
  }
}
```

Let’s break this down field by field:

code is a string that represents the status of the data transfer. Think of it like an HTTP status code.
success is a boolean that indicates whether the mutation was successful. This allows a coarse check by the client to know if there were failures.
message is a human-readable string that describes the result of the mutation. It is intended to be used in the UI of the product.
user is added by the implementing type UpdateUserEmailMutationResponse to return the newly updated user to the client.
If a mutation modifies multiple types (like our earlier example of "liking" a blog post), its implementing type can include a separate field for each type that's modified:

```sh
type LikePostMutationResponse implements MutationResponse {
  code: String!
  success: Boolean!
  message: String!
  post: Post
  user: User
}
```

Because our hypothetical likePost mutation modifies fields on both a Post and a User, its response object includes fields for both of those types. A response has the following structure:

```sh
{
  "data": {
    "likePost": {
      "code": "200",
      "success": true,
      "message": "Thanks!",
      "post": {
        "id": "123",
        "likes": 5040
      },
      "user": {
        "likedPosts": ["123"]
      }
    }
  }
}
```

Following this pattern provides a client with helpful, detailed information about the result of each requested operation. Equipped with this information, developers can better react to operation failures in their client code.

