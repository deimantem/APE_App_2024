> Note: Ich musste die "generic constraints" von `ILocalStorage` anpassen, um ausführen zu können.


| Möglich | Erreicht | Kriterium                                                                                                  |
| ------: | -------: | ---------------------------------------------------------------------------------------------------------- |
|      45 |       32 | **Summe**                                                                                                  |
|         |      4.9 | **Note**                                                                                                   |
|         |          | **Präsentation**                                                                                           |
|       1 |        1 | Die Präsentation hat eine Einführung.                                                                      |
|       1 |        1 | Die Präsentation ist sachlogisch aufgebaut.                                                                |
|       2 |        2 | Die wesentlichen Funktionen werden vorgeführt.                                                             |
|       1 |        1 | Die Zeit (10 Minuten) wird eingehalten                                                                     |
|         |          | **Daten auflisten**                                                                                        |
|       3 |        3 | Kann Daten für einen Entität in einer Liste darstellen.                                                    |
|       2 |        0 | Die Daten werden per HTTPClient von einem Server geladen.                                                  |
|         |          | **Daten bearbeiten**                                                                                       |
|       5 |        5 | Diese Daten können vom Benutzer geändert und lokal gespeichert werden.                                     |
|       2 |        2 | Die Daten werden lokal persistiert und überleben das Neustart der Applikation.                             |
|       1 |        1 | Die Daten bestehen aus verschiedenen Typen (z.B. Datum, Text, Zahlen, Boolean, usw.).                      |
|       3 |        0 | Die Daten werden per HTTPClient zurück auf dem Server gespeichert.                                         |
|         |          | **Daten validieren**                                                                                       |
|       2 |        2 | Die Daten werden validiert (z.B. Textlänge, Datumsbereich, Zahlengrösse, Zwangsfelder usw.).               |
|       1 |        1 | Die Validierung verhindert das Speichern.                                                                  |
|         |          | **Daten erstellen**                                                                                        |
|       1 |        1 | Ein neues Entität kann erstellt werden.                                                                    |
|       2 |        0 | Die neuen Daten werden per HTTPClient zurück auf dem Server gespeichert.                                   |
|         |          | **Daten löschen**                                                                                          |
|       1 |        1 | Ein Entität kann gelöscht werden.                                                                          |
|       2 |        0 | Die Daten werden per HTTPClient zurück auf dem Server gelöscht.                                            |
|         |          | **Testing**                                                                                                |
|       5 |        3 | Das erstellen, ändern, und löschen von Entitäten wird möglichst ohne UI mit automatisierten Tests geprüft. |
|         |          | **Architektur**                                                                                            |
|       2 |        2 | Services werden für Logik eingesetzt.                                                                      |
|       2 |        2 | Abhängigkeiten zwischen Services werden mit DI / Constructor-injection konfiguriert.                       |
|       1 |        1 | Ein IOC Container wird dafür eingesetzt.                                                                   |
|       1 |        1 | Dasselbe Container wird auch in die Tests eingesetzt.                                                      |
|         |          | **Änderungen bestätigen**                                                                                  |
|       2 |        2 | Die App bestätigt Änderungen durch Popup-Dialogen.                                                         |
|       2 |        0 | Diese Abläufe werden mit automatisierten Tests geprüft.                                                    |
