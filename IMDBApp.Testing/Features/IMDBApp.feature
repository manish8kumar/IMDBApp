Feature: IMDB


@addMovie
Scenario: Adding Movie to the list
	Given I have a movie with title '<Name>',the year of release is '<Year_Of_Release>',the plot is '<Plot>',the actors are '<Actors>',the producer is '<Producer>'
	When I add the movie in IMDB app
	Then Response should be '<Response>'
	Examples: 
	| Name                     | Year_Of_Release | Plot                                                                                                                   | Actors                          | Producer            | Response                     |
	| The Shawshank Redemption | 1994            | Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency. | Tim Robbins , Morgan Freeman    | Niki Marvin         | Movie Added                  |
	|                          | 1994            | Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency. | Tim Robbins , Morgan Freeman    | Niki Marvin         | Movie Name cannot be empty   |
	| The Shawshank Redemption | -200            | Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency. | Tim Robbins , Morgan Freeman    | Niki Marvin         | Invalid Year Of Release      |
	| The Shawshank Redemption | 999             | Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency. | Tim Robbins , Morgan Freeman    | Niki Marvin         | Invalid Year Of Release      |
	| Redemption               | 1994            | Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency. |                                 | Niki Marvin         | List of actor cannot be empty|
	| The Shawshank Redemption | 1994            | Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency. | Tim Robbins , Morgan Freeman    |                     | Producer cannot be empty     |
	
@listMovie
Scenario: List all movies in app
	Given I have collection of movies
	When I fetch my movies
	Then I should have the following movies
	| Name                     | Year_Of_Release | Plot                                                                                                                             | Actors                     | Producer               |
	| Inception                | 2010            | A thief who enters the dreams of others to steal their secrets must plant an idea into someone's mind in order to return home.   | Tom Hanks, Meryl Streep    | Christopher Nolan      |