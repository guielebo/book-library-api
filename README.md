# Book Management Application

This is a book management application built with .NET 7, Docker, and Entity Framework. It allows users to perform CRUD operations on books and search through the book collection based on various criteria.

## Features

- CRUD operations on books
- Paginated and searchable book listings
- Filter search by author, title, ISBN, etc.

## Technologies

- .NET 7
- Docker
- Entity Framework
- SQL Server

## Getting Started

Follow these instructions to get the project up and running on your local machine for development and testing purposes.

### Prerequisites

- Docker
- .NET SDK 7

### Installation

1. Clone the repository:
```bash
   git clone <repository-url>
```

2. Run the application:
```
dotnet run --project Path/To/Project.csproj
```

### API Reference
Search Books Endpoint
Search for books using a specific term and paginate the results.

### Request
Method: GET
URL: /book
Query Parameters:
termSearch: Search term (e.g., part of the book title, author name).
pageNumber: The number of the results page you want to retrieve.
pageSize: The number of items per page.

```
curl -X 'GET' \
  'https://localhost:7117/book?termSearch=John&pageNumber=2&pageSize=10' \
  -H 'accept: */*'
```

Response
```
{
  "items": [
    {
      "bookId": 6,
      "title": "The Book Thief",
      "firstName": "Markus",
      "lastName": "Zusak",
      "totalCopies": 75,
      "copiesInUse": 11,
      "type": "Hardcover",
      "isbn": "1234567896",
      "category": "Mystery"
    },
    {
      "bookId": 7,
      "title": "The Chronicles of Narnia",
      "firstName": "C.S.",
      "lastName": "Lewis",
      "totalCopies": 100,
      "copiesInUse": 14,
      "type": "Paperback",
      "isbn": "1234567897",
      "category": "Sci-Fi"
    }
    ]
}
```
