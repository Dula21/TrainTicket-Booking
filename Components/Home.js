// src/components/Home.js
import React, { useState } from "react";
import { useHistory } from "react-router";
import TicketSearch from "./TicketSearch";
import "./Home.css";

const Home = () => {
  const [searchResults, setSearchResults] = useState([]);
  const [selectedTicket, setSelectedTicket] = useState(null);
  const history = useHistory();

  const handleSearch = (results) => {
    setSearchResults(results);
  };

  const handleBook = (ticket) => {
    setSelectedTicket(ticket);
    history.push("./TicketBooking", { ticket });
  };

  return (
    <div className="home-container">
      <h1>Welcome to the Railway Ticket Booking System</h1>
      <TicketSearch onSearch={handleSearch} />
      {searchResults.length > 0 && (
        <ul className="search-results">
          {searchResults.map((ticket) => (
            <li key={ticket.id} onClick={() => handleBook(ticket)}>
              <p>Train Name: {ticket.trainName}</p>
              <p>From: {ticket.from}</p>
              <p>To: {ticket.to}</p>
              <p>Seats: {ticket.seats}</p>
              <p>Seat Price: {ticket.seatPrice}</p>
              <p>Available Seats: {ticket.availableSeats}</p>
            </li>
          ))}
        </ul>
      )}
      {selectedTicket && <Booking ticket={selectedTicket} />}
    </div>
  );
};

export default Home;