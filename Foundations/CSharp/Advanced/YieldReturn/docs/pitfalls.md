# Common Pitfalls

## 1. Multiple Enumeration

Each enumeration reruns the method.

## 2. Deferred Exceptions

Exceptions happen during enumeration, not method call.

## 3. DbContext Disposal

Dangerous:

```csharp
using var db = new AppDbContext();

foreach(var x in db.Users)
{
    yield return x;
}
```

## 4. Hidden Execution Timing

Method body executes later.
