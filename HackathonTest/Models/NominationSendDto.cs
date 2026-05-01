namespace HackathonTest.Models
{
    public class NominationSendDto
    {
        public string? NomStatus { get; set; }
        public string? GisbStatus { get; set; }
        public string? SchedQty { get; set; }
        public string? TransType { get; set; }
        public string? QuantityTypeIndicator { get; set; }

        public string? StartedDate { get; set; }
        public string? EndDate { get; set; }
        public string? CreatedDate { get; set; }

        public string? Cycle { get; set; }
        public string? ContractNumber { get; set; }
        public string? RollNom { get; set; }

        public string? RecLocation { get; set; }
        public string? RecLocProp { get; set; }
        public string? RecLocId { get; set; }

        public string? UpName { get; set; }
        public string? UpIdProp { get; set; }
        public string? UpId { get; set; }
        public string? UpContractNumber { get; set; }

        public string? RecQty { get; set; }
        public string? RecRank { get; set; }

        public string? DelLoc { get; set; }
        public string? DelLocId { get; set; }
        public string? DelLocProp { get; set; }

        public string? DownName { get; set; }
        public string? DownIdProp { get; set; }
        public string? DownId { get; set; }
        public string? DownContractNumber { get; set; }

        public string? DelQuantity { get; set; }
        public string? DelRank { get; set; }

        public string? DealType { get; set; }
        public string? CapacityBlockId { get; set; }
        public string? PkgId { get; set; }

        public string? FuelPercent { get; set; }

        public string? ShipperSpecificId { get; set; }
        public string? NomTrackingId { get; set; }

        public string? NomSubmittedDateTime { get; set; }
        public string? NomQuickResponseDateTime { get; set; }

        public string? ReferenceNumber { get; set; }
        public string? AgentDuns { get; set; }
    }
}