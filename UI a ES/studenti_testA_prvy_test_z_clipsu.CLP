(deffacts studenti
(studenti Jozo Fero Hana Miso Kata)
(studenti Peto Hana))

(defrule miso
(studenti $?x Miso $?y)

=>
(bind ?z ?x Miso ?y)
(printout t "Zoznam studentov v ktorom je Miso" ?z)

)

(defrule modify1
 ?x<-(studenti Peto Hana)
=>
(modify ?x(studenti Peto Barbara))
)