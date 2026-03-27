# Smart Finance Tracker

A full-stack finance tracking app built with:

- ASP.NET Core Web API
- Vue 3 + TypeScript
- Tailwind CSS
- SQLite

----------------------------------------------------------------------------------------------------------------------------------
## Features

- Track income & expenses
- Savings & investment tracking
- Dashboard with insights
- Charts for spending analysis
- Transfer-based financial modelling

----------------------------------------------------------------------------------------------------------------------------------
## Setup

### Backend
- cd FinanceTracker.Api
- dotnet ef database update
- dotnet run

### Frontend
- cd finance-tracker-fe
- npm install
- npm run dev

----------------------------------------------------------------------------------------------------------------------------------
## Known Limitations

- Recurring transactions logic is partially implemented but not fully functional yet
- Recurring generation needs further refinement and testing
- No user authentication (single-user system)
- SQLite used for simplicity (not production-ready)
  
----------------------------------------------------------------------------------------------------------------------------------
## Future Improvements

- Fix and finalize recurring transaction automation
- Add support for multiple recurrence frequencies (weekly, yearly)
- Improve recurring logic with scheduled execution
- Add user authentication and multi-user support
- Enhance UI/UX for mobile responsiveness
- Add more advanced financial insights (savings rate, investment ratio)
- Integrate real investment value tracking 
- Add export functionality (CSV / PDF)
- Add more in-depth savings metrics and overviews
  
----------------------------------------------------------------------------------------------------------------------------------
## Notes

This project focuses on modelling real-world financial flows:
- Income
- Expenses
- Transfers (savings & investments)

Transfers are treated separately from expenses to ensure accurate financial insights.
