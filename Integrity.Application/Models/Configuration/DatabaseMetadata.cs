namespace Integrity.Application.Models.Configuration;

public sealed record DatabaseMetadata(
    string Provider,
    string DatabaseName,
    string DatabaseVersion
    //TODO string SchemaVersion
    );