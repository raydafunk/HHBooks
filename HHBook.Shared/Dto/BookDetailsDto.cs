namespace HHBook.Shared.Dto;

public record  BookDetailsDto(
        int ID, 
        string Title, 
        string Image, AuthorDto Author, 
        int NumPages, 
        string Formant, 
        string Description, 
        GenreDto[]  Genres
    );


    
    
