📚 Presentazione del Progetto
Il progetto è un e-commerce dedicato alla vendita e gestione di libri. Gli utenti possono registrarsi, esplorare un ampio catalogo di libri, autori e generi, gestire il proprio carrello e completare gli acquisti. La piattaforma permette inoltre l'aggiunta e la gestione di nuovi autori, generi e libri, garantendo un'esperienza completa sia per gli utenti che per gli amministratori.

🎯 Obiettivo del Progetto
Dimostrare competenze nella realizzazione di un'applicazione full-stack, gestendo in modo efficace la comunicazione tra frontend e backend, l'autenticazione degli utenti, la protezione delle rotte, e la persistenza dei dati in un database relazionale.

💻 Tecnologie Utilizzate
🖥️ Frontend
Angular: Per creare un'interfaccia utente dinamica e responsive.
SCSS & Bootstrap: Per la gestione del layout e dello stile, garantendo un design moderno e consistente.
HttpClient: Per gestire le richieste API verso il backend.
Reactive Forms: Per la gestione dei moduli di registrazione e modifica dei dati.
Gestione del routing: Implementata per permettere una navigazione fluida tra le diverse pagine del sito.
Responsive Design: Tutte le pagine sono state progettate per adattarsi perfettamente a schermi di diverse dimensioni, offrendo un'esperienza utente ottimale su dispositivi mobili, tablet e desktop.
⚙️ Backend
C# e ASP.NET Core: Per gestire la logica applicativa e fornire le API necessarie per il funzionamento del frontend.
Entity Framework Core: Per la gestione dell'accesso ai dati e l'interazione con il database SQL Server.
JWT (JSON Web Token): Per l'autenticazione e la protezione delle rotte.
Swagger: Per la documentazione e il testing delle API.
🗄️ Database
SQL Server: Utilizzato per memorizzare e gestire i dati relativi a utenti, libri, autori, generi e transazioni d'acquisto.
🚀 Funzionalità Principali
👤 Gestione Utente
Registrazione e login degli utenti.
Autenticazione tramite JWT per proteggere le rotte e le informazioni sensibili.
Protezione delle rotte, assicurando che solo gli utenti autorizzati possano accedere alle sezioni riservate.
📚 Gestione Libri
Visualizzazione e gestione di un catalogo di libri suddiviso per generi e autori.
Creazione, modifica ed eliminazione di libri, autori e generi (riservata agli amministratori).
🛒 E-commerce
Aggiunta di libri al carrello e completamento degli acquisti.
Gestione del profilo utente, con visualizzazione degli ordini effettuati.
🛠️ Struttura del Progetto
Interfaccia & Servizi: Implementazione di una chiara separazione tra componenti e servizi per un'applicazione modulare e mantenibile.
Gestione delle Immagini: Caricamento e gestione delle immagini tramite la directory wwwroot.
🔒 Sicurezza e Autenticazione
Protezione delle rotte grazie all'utilizzo di JWT e gestione dei permessi per utenti e amministratori.
Interceptor per mantenere l'autenticazione attiva durante la navigazione.
