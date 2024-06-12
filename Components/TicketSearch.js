// src/components/TicketSearch.js
import React, { useState } from "react";
import "./TicketSearch.css";

const TicketSearch = ({ onSearch }) => {
  const [from, setFrom] = useState("");
  const [to, setTo] = useState("");
  const [date, setDate] = useState("");

  const handleSubmit = (event) => {
    event.preventDefault();
    // Implement search logic here
    const results = [
      {
        id: 1,
        trainName: "Train 1",
        from: "City A",
        to: "City B",
        seats: 2,
        seatPrice: 50,
        availableSeats: 10,
      },
      {
        id: 2,
        trainName: "Train 2",
        from: "City A",
        to: "City B",
        seats: 3,
        seatPrice: 75,
        availableSeats: 5,
      },
    ];
    onSearch(results);
  };

  return (
    <form onSubmit={handleSubmit} className="ticket-search-form">
      <label htmlFor="from">From:</label>
      <input
        type="text"
        id="from"
        value={from}
        onChange={(event) => setFrom(event.target.value)}
      />
      <label htmlFor="to">To:</label>
      <input
        type="text"
        id="to"
        value={to}
        onChange={(event) => setTo(event.target.value)}
      />
      <label htmlFor="date">Date:</label>
      <input
        type="date"
        id="date"
        value={date}
        onChange={(event) => setDate(event.target.value)}
      />
      <button type="submit">Search</button>
    </form>
  );
};

export default TicketSearch;
