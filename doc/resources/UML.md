<details> 
<summary></summary>
custom_relations_uml
@startuml;
object Author {
    name = "string"
    email = "string"
}

object Article {
    authorId = "int"
    title = "string"
    body = "string"
}

object Comment {
    authorId = "int"
    articleId = "int"
    message = "string"
}

Author --|> Article : many
Article --|> Author : one
    
Article --|> Comment : many
Comment --|> Article : one

Author --|> Comment : many

@enduml
custom_relations_uml
</details>
