namespace BlazorApp1.DTO
{


        public class UsersResponse
        {
            public int Page { get; set; }
            public int PerPage { get; set; }
            public int Total { get; set; }
            public int TotalPages { get; set; }
            public List<UserData> Data { get; set; }
            public Support Support { get; set; }
        }

        public class UserData
        {
            public int Id { get; set; }
            public string email { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string Avatar { get; set; }
        }

        public class Support
        {
            public string Url { get; set; }
            public string Text { get; set; }
        }



  
}
