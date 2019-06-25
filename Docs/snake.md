# Snake
Provide information and control about the snake's movement

### Constructor Summary
| Constructor Signature              | Description                          |
| ---------------------------------- | ------------------------------------ |
| [`public Snake()`](#snake-default) | Create a new snake with 4 body units.|

### Method Summary
| Method Signature | Description |
| --- | --- |
| [`public void AddBody(Vector3 pos)`](#addbody) | Create a snake body GameObject and add it to the snake. |
| [`public void Move(Vector3 dir)`](#move) | Move the snake one unit in the direction of dir. |
| [`public bool Eats(Vector3 pos)`](#eats) | Determine if the snake has eaten the object at the given position. |
| [`public bool Contains(Vector3 pos)`](#contains) | Determine if the snake's body contains the given position. |
| [`public void Digest()`](#digest) | Set a bowel timer for the snake to be able to poop. |
| [`public bool Poops()`](#poops) | Determine if the snake needs to poop. |

## Constructors
Create a new snake.

### Snake (default)

```csharp
public Snake()
```

> Postconditions

New snakes have bodies 4 units long.

[Top](#Snake)

----------
## Methods
----------


### AddBody
```csharp
public void AddBody(Vector3 pos)
```
Create a snake body GameObject and add it to the snake.

> **Parameters**  

| name | Description                |
| ---- | -------------------------- |
| pos  | position of the body part. |
                                                                         
> **Preconditions**  

 pos doesn't overlap any other body parts, and continues from the tail
 of the snake.

> **Postconditions**  

 A new GameObject with a SpriteRenderer component has been added to the
 snake. The SpriteRenderer's sprite attribute should hold the appropriate
 sprite.

[Top](#snake)

---

### Move
Move the snake one unit in the direction of dir.

```csharp
public void Move(Vector3 dir)
```

> Parameters

| Name | Description |
| --- | --- |
| dir | direction to move in. 3rd dimension is ignored |

> Preconditions

dir is within the set {left, right, up, down}

> Postconditions

All parts of the snake are continuous and have not made any sudden
jumps in position.

[Top](#snake)

---

### Eats
Determine if the snake has eaten the object at the given position.

```csharp
public bool Eats(Vector3 pos)
```

> Parameters

| Name | Description |
| --- | --- |
| pos | position to compare |

> Postconditions

Eats will only compare the given position with the head of the snake.

[Top](#snake)

---

### Contains
Determine if the snake's body contains the given position.

```csharp
public bool Contains(Vector3 pos)
```

> Parameters

| Name | Description |
| --- | --- |
| pos | position to compare |

> Postconditions

Contains will compare the given position with the entire body of
the snake.

[Top](#snake)

---

### Digest
Set a bowel timer for the snake to be able to poop.

```csharp
public void Digest()
```

> Postconditions

If a bowel timer is already ticking, Digest() doesn't overwrite that
timer but instead adds another one. The timer should count for as many
frames as the snake is long minus one.

[Top](#snake)

----

### Poops
Determine if the snake needs to poop.

```csharp
public bool Poops()
```

> Postconditions

A snake will poop if a bowel timer has reached 0. This method will
also move bowel timers forward one tick.

[Top](#snake)

----
