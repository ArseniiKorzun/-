import { useState } from 'react'
import { api } from '../api.js'
import { useLoad } from '../useLoad.js'
import { TIME_ZONE_NOTE } from '../format.js'
import SessionList from './SessionList.jsx'

const empty = { date: '', maxPrice: '' }

export default function AfishaPage() {
  const [filters, setFilters] = useState(empty)
  const { date, maxPrice } = filters
  const sessions = useLoad(() => api.getSessions({ date, maxPrice }), [date, maxPrice])

  const change = (e) => setFilters({ ...filters, [e.target.name]: e.target.value })

  return (
    <section>
      <div className="page-toolbar">
        <h2>Upcoming sessions</h2>
        <span className="hint">{TIME_ZONE_NOTE} Sorted from nearest.</span>
      </div>

      <form className="filters" onSubmit={(e) => e.preventDefault()}>
        <label>
          Date (UTC)
          <input type="date" name="date" value={date} onChange={change} />
        </label>
        <label>
          Max price
          <input type="number" name="maxPrice" step="0.01" placeholder="Any" value={maxPrice} onChange={change} />
        </label>
        <button type="button" className="secondary" onClick={() => setFilters(empty)}>Reset</button>
      </form>

      {sessions.error && <p className="message error">{sessions.error}</p>}
      {sessions.loading && !sessions.data
        ? <p>Loading...</p>
        : sessions.data && (sessions.data.length
          ? <SessionList sessions={sessions.data} />
          : <p className="empty">No sessions match the filters.</p>)}
    </section>
  )
}
