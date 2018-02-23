# HATEOAS sandbox

```mermaid
graph LR
    User --> |many| Post
    Post --> |one| User
    
    Post --> |many| Comment
    Comment --> |one| Post

    User --> |many| Comment
```