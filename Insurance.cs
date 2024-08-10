public class Insurance
{
    public int InsuranceId { get; set; }
    public int CarId { get; set; }
    public int UserId { get; set; }
    public string CoverageDetails { get; set; }

    // Navigation properties
    public Car Car { get; set; }
    public List<DamageReport> DamageReports { get; set; }

    // Constructor to initialize coverage details
    public Insurance()
    {
        CoverageDetails = "This insurance will cover $60,000 worth of total chargeable damages. " +
                          "If the rental vehicle is involved in damaging third-party property/properties, " +
                          "the insurance will cover the total cost. " +
                          "If the rental vehicle is involved in an accident with or kills a third-party individual, " +
                          "the insurance will cover that individual's costs. " +
                          "If the rental car breaks down or has flat tires, the insurance will cover the cost. " +
                          "We will also cover the towing service." +
                          "Other damages to the car will be considered chargeable damages.";
    }

    public Insurance GetInsurance(int insuranceId, string coverageDetails)
    {
        // Implementation for getting insurance details
        return this;
    }
}
