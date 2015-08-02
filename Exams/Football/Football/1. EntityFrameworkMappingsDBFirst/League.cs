//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _1.EntityFrameworkMappingsDBFirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class League
    {
        public League()
        {
            this.InternationalMatches = new HashSet<InternationalMatch>();
            this.TeamMatches = new HashSet<TeamMatch>();
            this.Teams = new HashSet<Team>();
        }
    
        public int Id { get; set; }
        public string LeagueName { get; set; }
        public string CountryCode { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual ICollection<InternationalMatch> InternationalMatches { get; set; }
        public virtual ICollection<TeamMatch> TeamMatches { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
