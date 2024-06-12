import React, { useState } from "react";
import "./TicketBooking.css";

const TicketBooking = () => {
  const [trainName, setTrainName] = useState("");
  const [from, setFrom] = useState("");
  const [to, setTo] = useState("");
  const [seats, setSeats] = useState(0);
  const [seatPrice, setSeatPrice]= useState(0);
  const [total, setTotal] = useState(0);

  const handleSubmit = (event) => {
    event.preventDefault();
    const newTotal = seats * seatPrice;
    setTotal(newTotal);
  };

  return (
    <div className="ticket-booking-container">
      <h1>Railway Ticket Booking</h1>
      <form onSubmit={handleSubmit}>
        <label>
          Train Name:
          <input
            type="text"
            value={trainName}
            onChange={(event) => setTrainName(event.target.value)}
          />
        </label>
        <label>
          From:
          <input
            type="text"
            value={from}
            onChange={(event) => setFrom(event.target.value)}
          />
        </label>
        <label>
          To:
          <input
            type="text"
            value={to}
            onChange={(event) => setTo(event.target.value)}
          />
        </label>
        <label>
          Seats:
          <input
            type="number"
            value={seats}
            onChange={(event) => setSeats(event.target.value)}
          />
        </label>
        <label>
          Seat Price:
          <input
            type="number"
            value={seatPrice}
            onChange={(event) => setSeatPrice(event.target.value)}
          />
        </label>
        <button type="submit">Book Tickets</button>
      </form>
      {total > 0 && <p>Total: {total}</p>}
    </div>
  );
};

export default TicketBooking;
