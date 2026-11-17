import { useState } from 'react'
import { api } from '../api.js'
import { useLoad } from '../useLoad.js'
import { datePart, timePart, TIME_ZONE_NOTE } from '../format.js'
import SessionForm from './SessionForm.jsx'
import SessionList from './SessionList.jsx'

export default function SessionsAdminPage() {
  const movies = useLoad(api.getMovies, [])
  const sessions = useLoad(() => api.getSessions(), [])
  const [editing, setEditing] = useState(null)
  const [notice, setNotice] = useState(null)

  const save = async (data) => {
    const isNew = editing === 'new'
    if (isNew) await api.addSession(data)
    else await api.updateSession(editing.id, data)
    setEditing(null)
    setNotice({ type: 'success', text: isNew ? 'Session created.' : 'Session updated.' })
    sessions.reload()
  }

  const remove = async (s) => {
    if (!window.confirm(`Delete the session of "${s.movieTitle}" on ${datePart(s.startsAt)} ${timePart(s.startsAt)} UTC?`)) return
    setNotice(null)
    try {
      await api.deleteSession(s.id)
      setNotice({ type: 'success', text: 'Session deleted.' })
      sessions.reload()
    } catch (err) {
      setNotice({ type: 'error', text: err.message })
    }
  }

  return (
    <section>
      <div className="page-toolbar">
        <div>
          <h2>Manage sessions</h2>
          <span className="hint">{TIME_ZONE_NOTE}</span>
        </div>
        <button onClick={() => setEditing('new')} disabled={!movies.data?.length}>+ New session</button>
      </div>

      {movies.error && <p className="message error">{movies.error}</p>}
      {movies.data?.length === 0 && <p className="hint">Add a film first, then create sessions for it.</p>}
      {editing && (
        <SessionForm
          key={editing === 'new' ? 'new' : editing.id}
          movies={movies.data ?? []}
          session={editing === 'new' ? null : editing}
          onSubmit={save}
          onCancel={() => setEditing(null)}
        />
      )}
      {notice && <p className={`message ${notice.type}`}>{notice.text}</p>}
      {sessions.error && <p className="message error">{sessions.error}</p>}

      {sessions.loading && !sessions.data
        ? <p>Loading...</p>
        : sessions.data && (sessions.data.length
          ? (
            <SessionList
              sessions={sessions.data}
              actions={(s) => (
                <>
                  <button className="secondary" onClick={() => setEditing(s)}>Edit</button>
                  <button className="danger" onClick={() => remove(s)}>Delete</button>
                </>
              )}
            />
          )
          : <p className="empty">No upcoming sessions. Create one to get started.</p>)}
    </section>
  )
}
