
;;;======================================================
;;;   Monkees and Bananas Sample Problem
;;;
;;;     This is an extended version of a
;;;     rather common AI planning problem.
;;;     The point is for the monkee to find
;;;     and eat some bananas.
;;;
;;;     CLIPS Version 6.3 Example
;;;
;;;     To execute, merely load, reset and run.
;;;======================================================
             
;;;*************
;;;* TEMPLATES *
;;;*************

(deftemplate monkey 
   (slot location 
      (type SYMBOL) 
      (default green-couch))
   (slot on-top-of 
      (type SYMBOL) 
      (default floor)) 
   (slot holding 
      (type SYMBOL) 
      (default nothing)))

(deftemplate thing 
   (slot name 
      (type SYMBOL)
      (default ?NONE)) 
   (slot location 
      (type SYMBOL)
      (default ?NONE)) 
   (slot on-top-of 
      (type SYMBOL) 
      (default floor))
   (slot weight 
      (type SYMBOL) 
      (allowed-symbols light heavy)
      (default light)))
                    
(deftemplate chest 
   (slot name 
      (type SYMBOL)
      (default ?NONE)) 
   (slot contents 
      (type SYMBOL)
      (default ?NONE)) 
   (slot unlocked-by 
      (type SYMBOL)
      (default ?NONE)))
               
(deftemplate goal-is-to 
   (slot action 
      (type SYMBOL)
      (allowed-symbols hold unlock eat move on walk-to)
      (default ?NONE))   
   (slot target 
      (type SYMBOL)
      (default unspecified))
   (slot location 
      (type SYMBOL)
      (default unspecified)))
             
;;;***********
;;;* GLOBALS *             
;;;***********

(defglobal ?*output* = t)

;;;*************************
;;;* CHEST UNLOCKING RULES *
;;;*************************

(defrule hold-chest-to-put-on-floor "" 
  (goal-is-to (action unlock) (target ?chest))
  (thing (name ?chest) (on-top-of ~floor) (weight light))
  (monkey (holding ~?chest))
  (not (goal-is-to (action hold) (target ?chest)))
  =>
  (assert (goal-is-to (action hold) (target ?chest))))

(defrule put-chest-on-floor "" 
  (goal-is-to (action unlock) (target ?chest))
  ?monkey <- (monkey (location ?place) (on-top-of ?on) (holding ?chest))
  ?thing <- (thing (name ?chest))
  =>
  (printout ?*output* "Monkey throws the " ?chest " off the " 
                      ?on " onto the floor." crlf)
  (modify ?monkey (holding blank))
  (modify ?thing (location ?place) (on-top-of floor)))

(defrule get-key-to-unlock "" 
  (goal-is-to (action unlock) (target ?obj))
  (thing (name ?obj) (on-top-of floor))
  (chest (name ?obj) (unlocked-by ?key))
  (monkey (holding ~?key))
  (not (goal-is-to (action hold) (target ?key)))
  =>
  (assert (goal-is-to (action hold) (target ?key))))

(defrule move-to-chest-with-key "" 
  (goal-is-to (action unlock) (target ?chest))
  (monkey (location ?mplace) (holding ?key))
  (thing (name ?chest) (location ?cplace&~?mplace) (on-top-of floor))
  (chest (name ?chest) (unlocked-by ?key))
  (not (goal-is-to (action walk-to) (location ?cplace)))
  =>
  (assert (goal-is-to (action walk-to) (location ?cplace))))

(defrule unlock-chest-with-key "" 
  ?goal <- (goal-is-to (action unlock) (target ?name))
  ?chest <- (chest (name ?name) (contents ?contents) (unlocked-by ?key))
  (thing (name ?name) (location ?place) (on-top-of ?on))
  (monkey (location ?place) (on-top-of ?on) (holding ?key))
  =>
  (printout ?*output* "Monkey opens the " ?name " with the " ?key 
              " revealing the " ?contents "." crlf)
  (modify ?chest (contents nothing))
  (assert (thing (name ?contents) (location ?place) (on-top-of ?name)))
  (retract ?goal))

;;;*********************
;;;* HOLD OBJECT RULES * 
;;;*********************

(defrule unlock-chest-to-hold-object ""
  (goal-is-to (action hold) (target ?obj))
  (chest (name ?chest) (contents ?obj))
  (not (goal-is-to (action unlock) (target ?chest)))
  =>
  (assert (goal-is-to (action unlock) (target ?chest))))

(defrule use-ladder-to-hold ""
  (goal-is-to (action hold) (target ?obj))
  (thing (name ?obj) (location ?place) (on-top-of ceiling) (weight light))
  (not (thing (name ladder) (location ?place)))
  (not (goal-is-to (action move) (target ladder) (location ?place)))
  =>
  (assert (goal-is-to (action move) (target ladder) (location ?place))))

(defrule climb-ladder-to-hold ""
  (goal-is-to (action hold) (target ?obj))
  (thing (name ?obj) (location ?place) (on-top-of ceiling) (weight light))
  (thing (name ladder) (location ?place) (on-top-of floor))
  (monkey (on-top-of ~ladder))
  (not (goal-is-to (action on) (target ladder)))
  =>
  (assert (goal-is-to (action on) (target ladder))))

(defrule grab-object-from-ladder "" 
  ?goal <- (goal-is-to (action hold) (target ?name))
  ?thing <- (thing (name ?name) (location ?place) 
                     (on-top-of ceiling) (weight light))
  (thing (name ladder) (location ?place))
  ?monkey <- (monkey (location ?place) (on-top-of ladder) (holding blank))
  =>
  (printout ?*output* "Monkey grabs the " ?name "." crlf)
  (modify ?thing (location held) (on-top-of held))
  (modify ?monkey (holding ?name))
  (retract ?goal))

(defrule climb-to-hold "" 
  (goal-is-to (action hold) (target ?obj))
  (thing (name ?obj) (location ?place) (on-top-of ?on&~ceiling) (weight light))
  (monkey (location ?place) (on-top-of ~?on))
  (not (goal-is-to (action on) (target ?on)))
  =>
  (assert (goal-is-to (action on) (target ?on))))

(defrule walk-to-hold ""
  (goal-is-to (action hold) (target ?obj))
  (thing (name ?obj) (location ?place) (on-top-of ~ceiling) (weight light))
  (monkey (location ~?place))
  (not (goal-is-to (action walk-to) (location ?place)))
  =>
  (assert (goal-is-to (action walk-to) (location ?place))))

(defrule drop-to-hold ""
  (goal-is-to (action hold) (target ?obj))
  (thing (name ?obj) (location ?place) (on-top-of ?on) (weight light))
  (monkey (location ?place) (on-top-of ?on) (holding ~blank))
  (not (goal-is-to (action hold) (target blank)))
  =>
  (assert (goal-is-to (action hold) (target blank))))

(defrule grab-object "" 
  ?goal <- (goal-is-to (action hold) (target ?name))
  ?thing <- (thing (name ?name) (location ?place) 
                     (on-top-of ?on) (weight light))
  ?monkey <- (monkey (location ?place) (on-top-of ?on) (holding blank))
  =>
  (printout ?*output* "Monkey grabs the " ?name "." crlf)
  (modify ?thing (location held) (on-top-of held))
  (modify ?monkey (holding ?name))
  (retract ?goal))

(defrule drop-object ""  
  ?goal <- (goal-is-to (action hold) (target blank))
  ?monkey <- (monkey (location ?place) 
                     (on-top-of ?on) 
                     (holding ?name&~blank))
  ?thing <- (thing (name ?name))
  =>
  (printout ?*output* "Monkey drops the " ?name "." crlf)
  (modify ?monkey (holding blank))
  (modify ?thing (location ?place) (on-top-of ?on))
  (retract ?goal))

;;;*********************
;;;* MOVE OBJECT RULES * 
;;;*********************

(defrule unlock-chest-to-move-object "" 
  (goal-is-to (action move) (target ?obj))
  (chest (name ?chest) (contents ?obj))
  (not (goal-is-to (action unlock) (target ?chest)))
  =>
  (assert (goal-is-to (action unlock) (target ?chest))))

(defrule hold-object-to-move ""  
  (goal-is-to (action move) (target ?obj) (location ?place))
  (thing (name ?obj) (location ~?place) (weight light))
  (monkey (holding ~?obj))
  (not (goal-is-to (action hold) (target ?obj)))
  =>
  (assert (goal-is-to (action hold) (target ?obj))))

(defrule move-object-to-place "" 
  (goal-is-to (action move) (target ?obj) (location ?place))
  (monkey (location ~?place) (holding ?obj))
  (not (goal-is-to (action walk-to) (location ?place)))
  =>
  (assert (goal-is-to (action walk-to) (location ?place))))

(defrule drop-object-once-moved "" 
  ?goal <- (goal-is-to (action move) (target ?name) (location ?place))
  ?monkey <- (monkey (location ?place) (holding ?name))
  ?thing <- (thing (name ?name) (weight light))
  =>
  (printout ?*output* "Monkey drops the " ?name "." crlf)
  (modify ?monkey (holding blank))
  (modify ?thing (location ?place) (on-top-of floor))
  (retract ?goal))

(defrule already-moved-object ""
  ?goal <- (goal-is-to (action move) (target ?obj) (location ?place))
  (thing (name ?obj) (location ?place))
  =>
  (retract ?goal))

;;;***********************
;;;* WALK TO PLACE RULES *
;;;***********************

(defrule already-at-place "" 
  ?goal <- (goal-is-to (action walk-to) (location ?place))
  (monkey (location ?place))
  =>
  (retract ?goal))

(defrule get-on-floor-to-walk ""
  (goal-is-to (action walk-to) (location ?place))
  (monkey (location ~?place) (on-top-of ~floor))
  (not (goal-is-to (action on) (target floor)))
  =>
  (assert (goal-is-to (action on) (target floor))))

(defrule walk-holding-nothing ""
  ?goal <- (goal-is-to (action walk-to) (location ?place))
  ?monkey <- (monkey (location ~?place) (on-top-of floor) (holding blank))
  =>
  (printout ?*output* "Monkey walks to " ?place "." crlf)
  (modify ?monkey (location ?place))
  (retract ?goal))

(defrule walk-holding-object ""
  ?goal <- (goal-is-to (action walk-to) (location ?place))
  ?monkey <- (monkey (location ~?place) (on-top-of floor) (holding ?obj&~blank))
  =>
  (printout ?*output* "Monkey walks to " ?place " holding the " ?obj "." crlf)
  (modify ?monkey (location ?place))
  (retract ?goal))

;;;***********************
;;;* GET ON OBJECT RULES * 
;;;***********************

(defrule jump-onto-floor "" 
  ?goal <- (goal-is-to (action on) (target floor))
  ?monkey <- (monkey (on-top-of ?on&~floor))
  =>
  (printout ?*output* "Monkey jumps off the " ?on " onto the floor." crlf)
  (modify ?monkey (on-top-of floor))
  (retract ?goal))

(defrule walk-to-place-to-climb "" 
  (goal-is-to (action on) (target ?obj))
  (thing (name ?obj) (location ?place))
  (monkey (location ~?place))
  (not (goal-is-to (action walk-to) (location ?place)))
  =>
  (assert (goal-is-to (action walk-to) (location ?place))))

(defrule drop-to-climb "" 
  (goal-is-to (action on) (target ?obj))
  (thing (name ?obj) (location ?place))
  (monkey (location ?place) (holding ~blank))
  (not (goal-is-to (action hold) (target blank)))
  =>
  (assert (goal-is-to (action hold) (target blank))))

(defrule climb-indirectly "" 
  (goal-is-to (action on) (target ?obj))
  (thing (name ?obj) (location ?place) (on-top-of ?on))
  (monkey (location ?place) (on-top-of ~?on&~?obj) (holding blank))
  (not (goal-is-to (action on) (target ?on)))
  =>
  (assert (goal-is-to (action on) (target ?on))))

(defrule climb-directly ""  
  ?goal <- (goal-is-to (action on) (target ?obj))
  (thing (name ?obj) (location ?place) (on-top-of ?on))
  ?monkey <- (monkey (location ?place) (on-top-of ?on) (holding blank))
  =>
  (printout ?*output* "Monkey climbs onto the " ?obj "." crlf)
  (modify ?monkey (on-top-of ?obj))
  (retract ?goal))

(defrule already-on-object ""
  ?goal <- (goal-is-to (action on) (target ?obj))
  (monkey (on-top-of ?obj))
  =>
  (retract ?goal))

;;;********************
;;;* EAT OBJECT RULES * 
;;;********************

(defrule hold-to-eat ""
  (goal-is-to (action eat) (target ?obj))
  (monkey (holding ~?obj))
  (not (goal-is-to (action hold) (target ?obj)))
  =>
  (assert (goal-is-to (action hold) (target ?obj))))

(defrule satisfy-hunger ""
  ?goal <- (goal-is-to (action eat) (target ?name))
  ?monkey <- (monkey (holding ?name))
  ?thing <- (thing (name ?name))
  =>
  (printout ?*output* "Monkey eats the " ?name "." crlf)
  (modify ?monkey (holding blank))
  (retract ?goal ?thing))
 
;;;**********************
;;;* INITIAL STATE RULE * 
;;;**********************

(defrule startup ""
  =>
  (assert (monkey (location t5-7) (on-top-of green-couch) (holding blank)))
  (assert (thing (name green-couch) (location t5-7) (weight heavy)))
  (assert (thing (name red-couch) (location t2-2) (weight heavy)))
  (assert (thing (name big-pillow) (location t2-2) (on-top-of red-couch)))
  (assert (thing (name red-chest) (location t2-2) (on-top-of big-pillow)))
  (assert (chest (name red-chest) (contents ladder) (unlocked-by red-key)))
  (assert (thing (name blue-chest) (location t7-7) (on-top-of ceiling)))
  (assert (chest (name blue-chest) (contents bananas) (unlocked-by blue-key)))
  (assert (thing (name blue-couch) (location t8-8) (weight heavy)))
  (assert (thing (name green-chest) (location t8-8) (on-top-of ceiling)))
  (assert (chest (name green-chest) (contents blue-key) (unlocked-by red-key)))
  (assert (thing (name red-key) (location t1-3)))
  (assert (goal-is-to (action eat) (target bananas))))
