using Lab3.CodeFirst.Models;

namespace Lab3.CodeFirst.Services;

public interface IRightsValidator
{
    bool ValidateReadRights(Position position);
    bool ValidateWriteRights(Position position);
}