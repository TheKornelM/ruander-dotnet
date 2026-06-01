namespace RoutingApp.Endpoints;

public static class NoteEndpoints
{
    public static RouteGroupBuilder MapNotesApi(this RouteGroupBuilder group)
    {
        List<string> notes = new() { "Első jegyzet", "Tanulni kell a routingot" };

        group.MapGet("/{id:int}", (int id) =>
            id >= 0 && id < notes.Count
                ? Results.Ok(notes[id])
                : Results.NotFound("Nincs ilyen jegyzet."));

        group.MapPost("/", (string note) =>
        {
            notes.Add(note);
            return Results.Created($"/notes/{notes.Count - 1}", note);
        });

        return group;
    }
}