﻿namespace Sentinel.Domain.Entities.Base
{
    public class EntidadeBase
    {
        public long Id { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime UltimaAlteracao { get; set; } = DateTime.UtcNow;
    }
}
