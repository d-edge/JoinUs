#r "nuget: FSharp.Data"
#r "System.Xml.Linq.dll"

open FSharp.Data

[<Literal>]
let PlaylistUrl = "https://www.youtube.com/feeds/videos.xml?playlist_id=PLychnrjxWBViBfk5V68i6tVZDM5UhNhBB"

type YouTubeRssFeed = XmlProvider<PlaylistUrl>

type System.String with
    member this.SubstringUntil(sub: string) =
        match this.IndexOf(sub) with
        | -1 -> this
        | pos -> this.Substring(0, pos)

printf "# D-EDGE tech meetups\n\r\n\r"
let feed = YouTubeRssFeed.Load(PlaylistUrl)
feed.Entries 
|> Array.filter (fun item -> item.Author.Name = "D-EDGE Hospitality Solutions") 
|> Array.iter (fun item -> 
    printf "## %s\n\r\n\r[![thumbnail](%s)](%s)\n\r\n\r%s\n\r\n\r" 
            item.Title 
            item.Group.Thumbnail.Url
            item.Link.Href
            (match item.Group.Description with
            | None -> ""
            | Some d -> d.SubstringUntil"======= Table of Contents ======="))