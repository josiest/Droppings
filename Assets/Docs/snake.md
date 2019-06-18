# Snake
Provide information and control about the snake's movement

### Constructor Summary
| Constructor Signature             | Description        |
| --------------------------------- | ------------------ |
| [`public Snake()`](#constructors) | Create a new snake.|

### Method Summary
| Method Signature | Description |
| --- | --- |
| [`public void Move(Vector3 dir)`](#methods) | Move the snake one unit in the direction of dir. |
| [`public bool Eats(Vector3 pos)`](#methods) | Determine if the snake has eaten the object at the given position. |
| [`public bool Contains(Vector3 pos)`](#methods) | Determine if the snake's body contains the given position. |
| [`public void Digest()`](#methods) | Set a bowel timer for the snake to be able to poop. |
| [`public bool Poops()`](#methods) | Determine if the snake needs to poop. |

## Constructors

> `public Snake()`
>
> Create a new snake.
>
> New snakes have bodies 4 units long.
