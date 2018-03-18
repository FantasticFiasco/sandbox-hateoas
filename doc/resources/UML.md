<details> 
<summary></summary>
custom_relations_uml
@startuml;
object Author {
    name
    email
}

object Article {

}

object Comment {

}

Author --|> Article : many
Article --|> Author : one
    
Article --|> Comment : many
Comment --|> Article : one

Author --|> Comment : many

@enduml
custom_relations_uml
</details>
