﻿using System;
using System.Collections.Generic;

namespace CRUDOperations.Assetmanager;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? PostId { get; set; }

    public int? UserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
