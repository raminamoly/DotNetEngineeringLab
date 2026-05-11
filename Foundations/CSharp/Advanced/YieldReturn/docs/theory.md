# Yield Return Notes

## What is yield return?

yield return enables lazy iteration.

Instead of creating the entire collection in memory, items are generated one-by-one during enumeration.

---

## Key Concepts

- Deferred execution
- State machine generation
- Streaming
- Lazy evaluation

---

## Compiler Behavior

The compiler transforms iterator methods into hidden classes implementing:

- IEnumerable<T>
- IEnumerator<T>

---

## Benefits

- Lower memory usage
- Streaming support
- Infinite sequences
- Better pipeline processing

---

## Dangers

- Multiple enumeration
- Deferred exceptions
- DbContext lifetime issues
- Hidden execution timing
