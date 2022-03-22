namespace SmartCharging.Core.Exceptions;

public static class ExceptionMessages
{
    public const string GroupCouldNotBeFound = "Group could not be found.";
    public const string GroupMaxCapacityInAmps = "The capacity in Amps of a Group should always be great or equal " +
                                                 "to the sum of the Max current in Amps of the Connector " +
                                                 "of all Charge Stations in the Group.";
    public const string GroupCapacityIsNotEnough = "The capacity in Amps of a Group is not enough.";

    public const string ChargeStationCouldNotBeFound = "Charge Station could not be found";
    public const string ConnectorCouldNotBeFound = "Connector could not be found";
    public const string ConnectorYouCannotAddMoreThanFive = "You cannot add more than 5 connectors.";

}