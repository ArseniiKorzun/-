import { useState } from 'react'
import { fromInputValue, toInputValue } from '../format.js'

const blank = { movieId: '', startsAt: '', hallName: '', price: '', availableSeats: '' }

const toForm = (s) => ({
  movieId: String(s.movieId),
  startsAt: toInputValue(s.startsAt),
  hallName: s.hallName,
  price: String(s.price),
  availableSeats: String(s.availableSeats),
})

export default function SessionForm({ movies, session, onSubmit, onCancel }) {
  const [form, setForm] = useState(session ? toForm(session) : blank)
  const [error, setError] = useState(null)
  const [saving, setSaving] = useState(false)

  const change = (e) => setForm({ ...form, [e.target.name]: e.target.value })

  const submit = async (e) => {
    e.preventDefault()
    setSaving(true)
    setError(null)
    try {
      await onSubmit({
        movieId: Number(form.movieId),
        startsAt: fromInputValue(form.startsAt),
        hallName: form.hallName.trim(),
        price: Number(form.price),
        availableSeats: Number(form.availableSeats),
      })
    } catch (err) {
      setError(err.message)
    } finally {
      setSaving(false)
    }
  }

  return (
    <form className="panel session-form" onSubmit={submit}>
      <h3>{session ? 'Edit session' : 'New session'}</h3>
      <label>
        Film
        <select name="movieId" value={form.movieId} onChange={change} required>
          <option value="" disabled>Select a film</option>
          {movies.map((m) => <option key={m.id} value={m.id}>{m.title}</option>)}
        </select>
      </label>
      <label>
        Start (UTC)
        <input type="datetime-local" name="startsAt" value={form.startsAt} onChange={change} required />
      </label>
      <label>
        Hall
        <input name="hallName" placeholder="Hall 1" value={form.hallName} onChange={change} required maxLength={50} />
      </label>
      <label>
        Price
        <input type="number" name="price" step="0.01" placeholder="0.00" value={form.price} onChange={change} required />
      </label>
      <label>
        Available seats
        <input type="number" name="availableSeats" step="1" placeholder="0" value={form.availableSeats} onChange={change} required />
      </label>
      {error && <p className="message error">{error}</p>}
      <div className="auth-buttons">
        <button type="submit" disabled={saving}>{saving ? 'Saving...' : 'Save'}</button>
        <button type="button" className="secondary" onClick={onCancel}>Cancel</button>
      </div>
    </form>
  )
}
