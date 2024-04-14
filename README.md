Introduction: “Recipes for Everyone” is a web application developed using ASP.NET Core 6. It provides a platform for users to create, share, and discover recipes. The application utilizes SQL Server for data storage, Bootstrap for front-end design, and follows the Model-View-Controller (MVC) architectural pattern. It incorporates a structured data access layer and service layer for efficient data management and business logic implementation.
 
Functionality:
1.	User Authentication: Users can register for an account or log in using existing credentials. Authentication is implemented securely to protect user data.
 
2.	Recipe Creation: Registered users can create new recipes by providing details such as title, ingredients, instructions, and images.
 
3.	Public Recipe Sharing: Users have the option to mark their recipes as public. Public recipes are displayed in the "Public Recipes" tab, allowing other users to discover and view them.
 
4.	Recipe Comments: Users can comment on recipes to share feedback, tips, or ask questions. Comments are visible to other users viewing the recipe.
 
5.	Editing and Deleting Recipes: Recipe authors have the privilege to edit or delete their own recipes. This functionality ensures users can maintain and update their recipes as needed.
6.	Personal Recipe Management: Users have a dedicated tab to view all the recipes they have created. This tab provides easy access to manage and organize their recipes.
 
Architecture:
1.	Model-View-Controller (MVC): The application is structured following the MVC architectural pattern. This separation of concerns ensures clear division between presentation, data, and business logic layers.
2.	Data Access Layer: The data access layer is responsible for interacting with the database. It encapsulates database operations such as querying, updating, and deleting data. Entity Framework Core is used for object-relational mapping and database access.
3.	Service Layer: The service layer contains the business logic of the application. It orchestrates interactions between the data access layer and the presentation layer. Services handle tasks such as recipe creation, validation, commenting, and user authentication.

Technologies Used:
1.	ASP.NET Core 6: Framework for building web applications and APIs.
2.	SQL Server: Relational database management system for data storage.
3.	Bootstrap: Front-end framework for responsive and mobile-first web development.
4.	Entity Framework Core: Object-relational mapping framework for database access.
5.	C#: Primary programming language used for backend development.
6.	HTML/CSS/JavaScript: Languages for building the user interface and client-side functionality.
Future Enhancements:
1.	Implementing additional features such as recipe categorization and search functionality.
2.	Enhancing user profiles with avatar images and social sharing capabilities.
3.	Introducing a rating system for recipes based on user feedback.
4.	Improving accessibility and responsiveness for a seamless user experience across different devices.
Contact Information ->  kosta.ilev@gmail.com
