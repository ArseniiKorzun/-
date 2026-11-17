import { datePart, timePart } from '../format.js'

export default function SessionList({ sessions, showMovie = true, actions }) {
  return (
    <div className="table-wrap">
      <table className="sessions-table">
        <thead>
          <tr>
            {showMovie && <th>Film</th>}
            <th>Date (UTC)</th>
            <th>Time (UTC)</th>
            <th>Hall</th>
            <th>Price</th>
            <th>Seats left</th>
            {actions && <th />}
          </tr>
        </thead>
        <tbody>
          {sessions.map((s) => (
            <tr key={s.id}>
              {showMovie && <td><a href={`#/movies/${s.movieId}`}>{s.movieTitle}</a></td>}
              <td>{datePart(s.startsAt)}</td>
              <td>{timePart(s.startsAt)}</td>
              <td>{s.hallName}</td>
              <td>{Number(s.price).toFixed(2)}</td>
              <td>{s.availableSeats}</td>
              {actions && <td className="row-actions">{actions(s)}</td>}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}
