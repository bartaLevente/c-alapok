TimeTable

A TimeTable egy WinFormssal készült alkalmazás, amely MVC architechtúrára törekedve egy órarendtervezőt valósít meg. Lehetőségünk van tanórák hozzáadására, módosítására, törlésére, illetve hétről-hétre tudjuk léptetni a naptárat. Mindig az aktuális héthez tartozó  órákat jeleníti meg.

Az adatok tárolását, mentését, olvasását egy lessons.txt fájlon keresztül végzi, így a program bezárása/újraindítása után is megőrzi az eltárolt tanórákat. Órákat felvenni az "Add lesson" segítségével tudunk. Egy felugró ablakban meg tudjuk adni a részleteket, kiválaszthatjuk a kívánt színt, majd a "Done!"-ra kattintva bezáródik az ablak, és az órarend frissül. A már meglévő órákat rájuk jobb egérgombbal kattintva tudjuk törölni vagy módosítani. Mindig az aktuális hétnek megfelelő dátum látszik a naptár fölött, amelyet a mellette lévő nyilakkal lehet léptetni.

A kód felépítése:
	Model:
	- A Lesson osztály tartalmazza egy óra adait (pl: azonosító, kezdési dátum, ...), illetve ezen adatok alapján kiszámolja a címke elhelyezkedését és méreteit
	- A Schedule class egy Lesson-öket tartalmazó listában tárolja az órákat, illetve kezeli ezt a listát a szükséges függvényekkel (get, add, delete, ..). Megvalósítja a fájlkezelést.

	Controller:
	- A Controller osztály kommunikál a Schedule-lel és a fő Form-mal, kiadja a szükséges utasításokat.

	View:
	- A Form1 osztály a fő adatok megjelenítéséért szolgál, kirajzolja a komponenseket, elhelyezi a feliratokat.
	- A Form2 a hozzáadás/módosítást kezeli. Kétféleképpen lehet példányosítani, innen tudja, hogy melyik műveletet kell elvégeznie. Hozzáadáskor üres formot jelenít meg, viszont módosításkor kitölti az eredti adatokkal.




