---
theme: gaia
class:
  - lead
paginate: true
marp: true
backgroundImage: url(hexagon.svg)
style: |
  .columns {
    display: grid;
    grid-template-columns: repeat(2, minmax(0, 1fr));
    gap: 1rem;
  }
---

# **Commands, Queries, and Façades**

Lukáš Daubner

---

# Contents

* CQRS (Command Query Responsibility Segregation) Pattern
* MediatR Library
* Façade Pattern

---

# **CQRS (Command Query Responsibility Segregation)**

---

# Why CQRS

* Models the reads (queries) and changes (commands) independently
* Enables separation of "read" and "write" data store
* Independent scaling of reads/writes
* Optimization of data schemes

---

# Without CQRS

<!--
footer: Image by Martin Fowler (https://www.martinfowler.com/bliki/CQRS.html)
-->

![Architecture without CQRS showing single interface and model for both reads and writes to a database.](without-cqrs.png)

---

# With CQRS

<!--
footer: Image by Martin Fowler (https://www.martinfowler.com/bliki/CQRS.html)
-->

![Architecture with CQRS showing separate interface and model for reads and writes to a database.](with-cqrs.png)

---

<!--
footer: ""
-->

# Query

* Encapsulated query to a data store
* Does not change a state
* Plain object
* Encapsulates what you are asking for
* Similar concept to QueryObject, but it is typically purpose-made

---

# Command

* Does change a state
* Plain object
* Does not return value
* Encapsulates what are you want to do in the app

---

# When to use CQRS


<div class="columns">
<div>

## Dos

* Collaborative, parallel apps
* Reads and writes are disproportional
* Interface is task-focused 
* Different model versions
* Integration with other systems
* Event Sourcing

</div>
<div>

## Don'ts

* Simple app
* Interface is CRUD-focused

</div>
</div>

---

# Implementing CQRS

* 

---

# **MediatR**

---

# **Façade**

---

# How to write slides

Split pages by horizontal ruler (`---`). It's very simple! :satisfied:

```markdown
# Slide 1

foobar

---

# Slide 2

foobar
```