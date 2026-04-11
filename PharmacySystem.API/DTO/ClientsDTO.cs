namespace PharmacySystem.API.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Client_Name { get; set; }
        public string Client_Phone { get; set; }
        public string Client_Address { get; set; }
    }

    public class CreateClientDto
    {
        public string Client_Name { get; set; }
        public string Client_Phone { get; set; }
        public string Client_Address { get; set; }
    }

    public class UpdateClientDto
    {
        public string Client_Name { get; set; }
        public string Client_Phone { get; set; }
        public string Client_Address { get; set; }
    }
}
